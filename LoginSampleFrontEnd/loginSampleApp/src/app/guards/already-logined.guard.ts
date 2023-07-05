import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const alreadyLoginedGuard: CanActivateFn = async (route, state) => {

  const authService : AuthService = inject(AuthService);
  const router:Router = inject(Router);
  
  if(await authService.isAuthenticated()){
    alert("Zaten Giriş Yaptınız.");
    router.navigate(['']);
    return false;
  }
  return true;
};
