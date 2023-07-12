import { CanActivateFn, Route, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const roleGuard: CanActivateFn = async (route, state) => {

  
  const authService : AuthService = inject(AuthService);
  const router:Router = inject(Router);

  if((await authService.isAuthenticated()) === false){    
    return false;
  }
  
  if(route.data['roles'] === undefined || route.data['roles'] ===  null){
    console.warn("Router role guard içeriyor ancak role içermiyor !!");    
    notAllowed(router);
    return false;
  }

  const requiredRoles: string[] = route.data['roles'];  
  const userRoles:string[] = authService.getUserRoles();

  // Check if user has requiredRole
  for (let i = 0; i < userRoles.length; i++) {
    if(requiredRoles.includes(userRoles[i])){
      return true;          
    }
  }

  notAllowed(router);
  return false;
};

function notAllowed(router:Router):void{  

  alert('Yetkiniz Bulunmuyor.');
  router.navigate(['']);
}


