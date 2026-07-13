import { Component, OnInit, inject } from '@angular/core';

import { CommonModule } from '@angular/common';

import Swal from 'sweetalert2';

import { OrderService } from '../../services/order';

import { Order } from '../../models/order';

@Component({

selector:'app-orders',

standalone:true,

imports:[CommonModule],

templateUrl:'./orders.html',

styleUrl:'./orders.css'

})

export class OrdersComponent implements OnInit{

private orderService=inject(OrderService);

orders:Order[]=[];

ngOnInit(){

this.loadOrders();

}

loadOrders(){

this.orderService.getOrders()

.subscribe({

next:data=>{

this.orders=data;

}

});

}

cancel(order:Order){

if(order.status!="Pending"){

return;

}

this.orderService.cancel(order.id)

.subscribe(()=>{

Swal.fire(

'Cancelled',

'Order Cancelled Successfully',

'success'

);

this.loadOrders();

});

}

}