import { CanActivateFn } from '@angular/router';

import { inject } from '@angular/core';

import { Router } from '@angular/router';

import { jwtDecode } from 'jwt-decode';

export const adminGuard:CanActivateFn=()=>{

const router=inject(Router);

const token=localStorage.getItem("token");

if(!token){

router.navigate(["/login"]);

return false;

}

const user:any=jwtDecode(token);

if(user.role==="Admin"){

return true;

}

router.navigate(["/"]);

return false;

};