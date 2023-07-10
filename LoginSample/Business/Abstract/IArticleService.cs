using Core.Results;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Abstract;

public interface IArticleService
{
    IResult Create(ArticleDto article);
    IResult Delete(int articleId);
    IResult Update(int articleId, ArticleDto updatedArticle);
    IDataResult<ArticleDto> Get(int articleId);
    IDataResult<IEnumerable<ArticleDto>> GetAll(int page, int size);

}