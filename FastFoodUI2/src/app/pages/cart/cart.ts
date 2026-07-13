import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import Swal from 'sweetalert2';

import { CartService } from '../../services/cart';

import { CartResponse } from '../../models/cart';

@Component({

selector:'app-cart',

standalone:true,

imports:[CommonModule],

templateUrl:'./cart.html',

styleUrl:'./cart.css'

})

export class CartComponent implements OnInit{

private cartService=inject(CartService);

private router=inject(Router);

cart?:CartResponse;

loading=true;

ngOnInit(){

this.loadCart();

}

loadCart(){

this.loading=true;

this.cartService.getCart()

.subscribe({

next:(res:any)=>{

this.cart=res;

this.loading=false;

},

error:()=>{

this.loading=false;

}

});

}

increase(item:any){

this.cartService.update(

item.id,

item.quantity+1

).subscribe(()=>{

this.loadCart();

});

}

decrease(item:any){

if(item.quantity==1){

return;

}

this.cartService.update(

item.id,

item.quantity-1

).subscribe(()=>{

this.loadCart();

});

}

remove(item:any){

Swal.fire({

title:'Remove Item?',

icon:'warning',

showCancelButton:true

}).then(result=>{

if(result.isConfirmed){

this.cartService.remove(

item.id

).subscribe(()=>{

this.loadCart();

});

}

});

}

checkout(){

this.router.navigate([

'/checkout'

]);

}

}