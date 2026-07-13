import { Component } from '@angular/core';

import { FormsModule } from '@angular/forms';

import { Router } from '@angular/router';

import Swal from 'sweetalert2';

import { AuthService } from '../../services/auth';

@Component({

selector:'app-login',

standalone:true,

imports:[FormsModule],

templateUrl:'./login.html',

styleUrl:'./login.css'

})

export class LoginComponent{

email='';

password='';

loading=false;

constructor(

private auth:AuthService,

private router:Router

){}

login(){

this.auth.login({

email:this.email,

password:this.password

}).subscribe({

next:(res)=>{

if(res.isSuccess){

this.auth.saveToken(

res.token

);

Swal.fire({

icon:'success',

title:'Success',

text:res.message,

timer:1500,

showConfirmButton:false

});

this.router.navigate(['/']);

}

},

error:(err)=>{

Swal.fire({

icon:'error',

title:'Login Failed',

text:err.error.message

});

}

});
}

}