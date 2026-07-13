import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import Swal from 'sweetalert2';

import { FoodService } from '../../../services/food';

@Component({
selector:'app-add-food',
standalone:true,
imports:[FormsModule],
templateUrl:'./add-food.html'
})
export class AddFoodComponent{

food:any={

name:'',
description:'',
price:0,
categoryId:0,
imageUrl:''

};

constructor(

private service:FoodService,
private router:Router

){}

save(){

this.service.create(this.food).subscribe({

next:()=>{

Swal.fire(
'Success',
'Food Added',
'success'
);

this.router.navigate(['/admin/foods']);

}

});

}

}