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

    public IResult Create(ArticleDto articleDto)
    {
        var result = BusinessRules.Run(
            CheckIfRequiredParametersNull(articleDto)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);

        ValidationResult validationResult = _validator.Validate(_mapper.Map<ArticleDto,Article>(articleDto));
        if (!validationResult.IsValid)
            return new ErrorResult(validationResult.Errors.FirstOrDefault().ErrorMessage);

        var articleToAdd = new Article()
        {
            CreatorId = GetLoginedUserId(),
            Title = articleDto.Title,
            Content = articleDto.Content,
        };
        _articleDal.Create(articleToAdd);
        return new SuccessResult(Messages.ArticleCreateSuccess);
    }

    public IResult Delete(int articleId)
    {

        var result = BusinessRules.Run(
            CheckIfArticleExistInDb(articleId),
            CheckIfCreatorOrAdminTryingToDelete(articleId)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);

        var articleToDelete = _articleDal.Get(a => a.Id == articleId);
        articleToDelete.DeletedBy = GetLoginedUserId();
        _articleDal.Delete(articleToDelete);
        return new SuccessResult(Messages.ArticleDeleteSuccess);
    }

    public IResult Update(int articleId, ArticleDto updatedArticle)
    {
        var result = BusinessRules.Run(
            CheckIfArticleExistInDb(articleId),
            CheckIfCreatorTryingModify(articleId)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);

        var articleToUpdate = _articleDal.Get(a => a.Id == articleId);
        articleToUpdate.Title = updatedArticle.Title;
        articleToUpdate.Content = updatedArticle.Content;
        articleToUpdate.UpdatedAt = DateTime.Now;

        _articleDal.Update(articleToUpdate);
        return new SuccessResult(Messages.ArticleUpdateSuccess);
    }

    public IDataResult<ArticleDto> Get(int articleId)
    {
        var article = _articleDal.Get(a => a.Id == articleId);

        if (article == null)
            return new ErrorDataResult<ArticleDto>(Messages.ArticleNotFound);

        return new SuccessDataResult<ArticleDto>(_mapper.Map<Article, ArticleDto>(article));
    }

    public IDataResult<IEnumerable<ArticleDto>> GetAll(int page = 0, int size = 10)
    {
        var articles = _articleDal.GetAll(null, page, size);

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

    private IResult CheckIfArticleExistInDb(int articleId)
    {
        var article = _articleDal.Get(a => a.Id == articleId);

        if (article == null)
            return new ErrorResult(Messages.ArticleNotFound);

        return new SuccessResult();
    }

    private IResult CheckIfCreatorTryingModify(int articleId)
    {
        var article = _articleDal.Get(a => a.Id == articleId);

        if (article == null)
            return new ErrorResult(Messages.ArticleNotFound);

        var loginedUserId = GetLoginedUserId();
        if (!IsLoginedUserTryingModify(article))
            return new ErrorResult(Messages.AnArticleAbleToModifyByOwner);

        return new SuccessResult();
    }

    private IResult CheckIfCreatorOrAdminTryingToDelete(int articleId)
    {
        var article = _articleDal.Get(a => a.Id == articleId);

        if (article == null)
            return new ErrorResult(Messages.ArticleNotFound);

        var loginedUserId = GetLoginedUserId();
        var loginedUserRoles = _userRoleDal.GetUserRoles(loginedUserId);

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