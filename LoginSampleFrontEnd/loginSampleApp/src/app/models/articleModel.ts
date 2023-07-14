export interface ArticleModel{
    id:number,
    title:string,
    content:string,
    createdAt:Date,
    creatorName:string;
    categories:string[];
}