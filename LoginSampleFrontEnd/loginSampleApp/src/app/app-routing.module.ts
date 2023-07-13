import { NgModule } from '@angular/core';
import { LoginComponent } from './components/login/login.component';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { UserComponent } from './components/user/user.component';
import { loginGuard } from './guards/login.guard';
import { alreadyLoginedGuard } from './guards/already-logined.guard';
import { HomeComponent } from './components/home/home.component';
import { CategoryComponent } from './components/category/category.component';
import { roleGuard } from './guards/role.guard';
import { AuthorizationRoles } from './helpers/authorizationRoles';
import { CategoryAddComponent } from './components/category-add/category-add.component';
import { RoleComponent } from './components/role/role.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { UserManagementComponent } from './components/user-management/user-management.component';

const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: "login", component: LoginComponent, canActivate: [alreadyLoginedGuard] },
  { path: "register", component: RegisterComponent, canActivate: [alreadyLoginedGuard] },
  { path: "user", component: UserComponent, canActivate: [loginGuard] },
  { path: "categories", component: CategoryComponent },

  { path: "admin/manage", component: AdminPanelComponent, canActivate: [loginGuard, roleGuard], data: {roles: [AuthorizationRoles.Admin]} },
  { path: "admin/manage/users", component: UserManagementComponent, canActivate: [loginGuard, roleGuard], data: {roles: [AuthorizationRoles.Admin]} },
  { path: "admin/manage/categories", component: CategoryAddComponent, canActivate: [loginGuard, roleGuard], data: {roles: [AuthorizationRoles.Admin]} },
  { path: "admin/manage/roles", component: RoleComponent, canActivate: [loginGuard, roleGuard], data: {roles: [AuthorizationRoles.Admin]} },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
