import { Component } from '@angular/core';

import { FormsModule } from '@angular/forms';

import { Router } from '@angular/router';

import Swal from 'sweetalert2';

import { AuthService } from '../../services/auth';

@Component({

selector:'app-register',

standalone:true,

imports:[FormsModule],

templateUrl:'./register.html',

styleUrl:'./register.css'

})

export class RegisterComponent{

fullName='';

email='';

password='';

constructor(

private auth:AuthService,

private router:Router

){}

register(){

this.auth.register({

fullName:this.fullName,

email:this.email,

password:this.password

})

.subscribe({

next:()=>{

Swal.fire(

'Success',

'Registration Completed',

'success'

);

this.router.navigate([

'/login'

]);

},

error:()=>{

Swal.fire(

'Error',

'Registration Failed',

'error'

);

}

});

}

}