import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AppRoutingModule } from './app-routing.module';
import { RegisterComponent } from './components/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { DatePipe } from '@angular/common';
import { UserComponent } from './components/user/user.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    UserComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    CookieService,
    DatePipe, 
    {provide:HTTP_INTERCEPTORS, useClass:AuthInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
