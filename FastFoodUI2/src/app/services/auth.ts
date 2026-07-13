import { Injectable, inject } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Router } from '@angular/router';

import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

import { Login } from '../models/login';

import { Register } from '../models/register';

import { AuthResponse } from '../models/auth-response';

@Injectable({

providedIn:'root'

})

export class AuthService{

private http=inject(HttpClient);

private router=inject(Router);

private api=environment.apiV1+"/Auth";

login(data:Login):Observable<AuthResponse>{

return this.http.post<AuthResponse>(

`${this.api}/login`,

data

);

}

register(data:Register){

return this.http.post(

`${this.api}/register`,

data

);

}

saveToken(token:string){

localStorage.setItem(

"token",

token

);

}

getToken(){

return localStorage.getItem(

"token"

);

}

isLoggedIn(){

return this.getToken()!=null;

}

logout(){

localStorage.clear();

this.router.navigate([

"/login"

]);

}

}