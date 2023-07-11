using System.Security.Claims;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Business.Validation;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Core.Entity.Temp;
using FluentValidation.Results;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Business.Concrete;

public class ArticleService : IArticleService
{
    private readonly IArticleDal _articleDal;
    private readonly ArticleForCreateValidator _validator;
    private readonly IUserRoleDal _userRoleDal;
    private readonly IMapper _mapper;

    public ArticleService(IArticleDal articleDal, ArticleForCreateValidator validator, IUserRoleDal userRoleDal, IMapper mapper)
    {
        _articleDal = articleDal;
        _validator = validator;
        _userRoleDal = userRoleDal;
        _mapper = mapper;
    }

    public async Task<IResult> CreateAsync(ArticleDto articleDto)
    {
        var result = BusinessRules.Run(
            CheckIfRequiredParametersNull(articleDto)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);
                

        ValidationResult validationResult = await _validator.ValidateAsync(_mapper.Map<ArticleDto,Article>(articleDto));
        if (!validationResult.IsValid)
            return new ErrorResult(validationResult.Errors.FirstOrDefault().ErrorMessage);

        var articleToAdd = new Article()
        {
            CreatorId = GetLoginedUserId(),
            Title = articleDto.Title,
            Content = articleDto.Content,
        };
        await _articleDal.CreateAsync(articleToAdd);
        return new SuccessResult(Messages.ArticleCreateSuccess);
    }

    public async Task<IResult> DeleteAsync(int articleId)
    {

        var result = BusinessRules.Run(
            await CheckIfArticleExistInDbAsync(articleId),
            await CheckIfCreatorOrAdminTryingToDeleteAsync(articleId)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);

        var articleToDelete = await _articleDal.GetAsync(a => a.Id == articleId);
        articleToDelete.DeletedBy = GetLoginedUserId();
        await _articleDal.DeleteAsync(articleToDelete);
        return new SuccessResult(Messages.ArticleDeleteSuccess);
    }

    public async Task<IResult> UpdateAsync(int articleId, ArticleDto updatedArticle)
    {
        var result = BusinessRules.Run(
            await CheckIfArticleExistInDbAsync(articleId),
            await CheckIfCreatorTryingModifyAsync(articleId)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);

        var articleToUpdate = await _articleDal.GetAsync(a => a.Id == articleId);
        articleToUpdate.Title = updatedArticle.Title;
        articleToUpdate.Content = updatedArticle.Content;
        articleToUpdate.UpdatedAt = DateTime.Now;

        await _articleDal.UpdateAsync(articleToUpdate);
        return new SuccessResult(Messages.ArticleUpdateSuccess);
    }

    public async Task<IDataResult<ArticleDto>> GetAsync(int articleId)
    {
        var article = await _articleDal.GetAsync(a => a.Id == articleId);

        if (article == null)
            return new ErrorDataResult<ArticleDto>(Messages.ArticleNotFound);

        return new SuccessDataResult<ArticleDto>(_mapper.Map<Article, ArticleDto>(article));
    }

    public async Task<IDataResult<IEnumerable<ArticleDto>>> GetAllAsync(int page = 0, int size = 10)
    {
        var articles = await _articleDal.GetAllAsync(null, page, size);

        return new SuccessDataResult<IEnumerable<ArticleDto>>(_mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDto>>(articles));
    }

    private IResult CheckIfRequiredParametersNull(ArticleDto article)
    {
        if (string.IsNullOrWhiteSpace(article.Title))
            return new ErrorResult(Messages.TitleCannotBeNull);

        if(string.IsNullOrWhiteSpace(article.Content))
            return new ErrorResult(Messages.ContentCannotBeNull);

        return new SuccessResult();
    }

    private async Task<IResult> CheckIfArticleExistInDbAsync(int articleId)
    {
        var article = await _articleDal.GetAsync(a => a.Id == articleId);

        if (article == null)
            return new ErrorResult(Messages.ArticleNotFound);

        return new SuccessResult();
    }

    private async Task<IResult> CheckIfCreatorTryingModifyAsync(int articleId)
    {
        var article = await _articleDal.GetAsync(a => a.Id == articleId);

        if (article == null)
            return new ErrorResult(Messages.ArticleNotFound);

        var loginedUserId = GetLoginedUserId();
        if (!IsLoginedUserTryingModify(article))
            return new ErrorResult(Messages.AnArticleAbleToModifyByOwner);

        return new SuccessResult();
    }

    private async Task<IResult> CheckIfCreatorOrAdminTryingToDeleteAsync(int articleId)
    {
        var article = await _articleDal.GetAsync(a => a.Id == articleId);

        if (article == null)
            return new ErrorResult(Messages.ArticleNotFound);

        var loginedUserId = GetLoginedUserId();
        var loginedUserRoles = await _userRoleDal.GetUserRolesAsync(loginedUserId);

        if (!loginedUserRoles.Contains(AuthorizationRoles.Admin)
            && !IsLoginedUserTryingModify(article))
            return new ErrorResult(Messages.AnArticleAbleToModifyByOwner);

        return new SuccessResult();
    }

    private int GetLoginedUserId()
    {
        var loginedUserId = LoginedUser.ClaimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Name);
        return int.Parse(loginedUserId);
    }

    private bool IsLoginedUserTryingModify(Article article)
    {
        var loginedUserId = GetLoginedUserId();
        return article.CreatorId == loginedUserId;
    }
}