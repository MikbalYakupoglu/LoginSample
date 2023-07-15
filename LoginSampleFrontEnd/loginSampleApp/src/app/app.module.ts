import { NgModule, inject } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AppRoutingModule } from './app-routing.module';
import { RegisterComponent } from './components/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { DatePipe } from '@angular/common';
import { UserComponent } from './components/user/user.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { HomeComponent } from './components/home/home.component';
import { ArticleComponent } from './components/article/article.component';
import { CategoryComponent } from './components/category/category.component';
import { RoleComponent } from './components/role-manage/role.component';
import { UserManagementComponent } from './components/user-management/user-management.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthService } from './services/auth.service';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CategoryAddComponent } from './components/category-management/category-add.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { ArticleManagementComponent } from './components/article-management/article-manage/article-management.component';
import { ArticleAddComponent } from './components/article-management/article-add/article-add.component';
import { ArticleUpdateComponent } from './components/article-management/article-update/article-update.component';

// const authService = inject(AuthService);

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    UserComponent,
    HomeComponent,
    ArticleComponent,
    CategoryComponent,
    RoleComponent,
    UserManagementComponent,
    NavbarComponent,
    CategoryAddComponent,
    AdminPanelComponent,
    ArticleManagementComponent,
    ArticleAddComponent,
    ArticleUpdateComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        // tokenGetter: authService.tokenGetter,
        allowedDomains: ['http://localhost:4200/categories']
      }
    })
  ],
  providers: [
    CookieService,
    DatePipe, 
    {provide:HTTP_INTERCEPTORS, useClass:AuthInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
