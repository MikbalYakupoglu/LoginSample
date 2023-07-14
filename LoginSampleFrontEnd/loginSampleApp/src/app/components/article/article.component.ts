import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ArticleModel } from 'src/app/models/articleModel';
import { ArticleService } from 'src/app/services/article.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {

  articles: ArticleModel[];
  articleDetails: ArticleModel;
  isDetails: boolean = false;

  constructor(private activatedRoute: ActivatedRoute, private articleService: ArticleService,
    private router:Router) { }

  ngOnInit(): void {
    this.manageRoute();
  }

  manageRoute() {
    this.activatedRoute.params.subscribe(params => {      
      if (params['categoryName']) {
        this.getArticlesByCategory(params['categoryName']);
      }
      else if (params['articleId']) {
        this.getArticle(params['articleId']);
      }
      else {
        this.getAllArticles();        
      }

    })
  }

  getArticle(articleId: number) {
    this.articleService.getArticle(articleId).subscribe((res) => {
      this.articleDetails = res.data;
      this.isDetails = true;
    })
  }

  getArticlesByCategory(categoryName: string) {    
    this.articleService.getAllArticlesByCategory(categoryName).subscribe((res) => {      
      this.articles = res.data;
      this.isDetails = false;
    },
    (errorRes) => {
      alert(errorRes.error.message)
      this.router.navigate(['']);
    })
  }

  getAllArticles() {
    this.articleService.getAllArticles().subscribe((res) => {
      this.articles = res.data;
      this.isDetails = false;
    })
  }

  routeToArticleDetails(articleId:number){
    this.router.navigate(['./articleDetails', articleId]);
  }

  getCreateDate() :string{
    const currentDate = new Date(this.articleDetails.createdAt);
    
    const day = currentDate.getDate();
    const month = currentDate.getMonth() + 1;
    const year = currentDate.getFullYear();

    const formattedDate = `${day}/${month}/${year}`;
    return formattedDate;
  }

}
