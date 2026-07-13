import { Component, inject } from '@angular/core';

import { FormsModule } from '@angular/forms';

import { Router } from '@angular/router';

import Swal from 'sweetalert2';

import { OrderService } from '../../services/order';

@Component({

  selector: 'app-checkout',

  standalone: true,

  imports: [FormsModule],

  templateUrl: './checkout.html',

  styleUrl: './checkout.css'

})

export class CheckoutComponent {

  private orderService = inject(OrderService);

  private router = inject(Router);

  deliveryAddress = '';

  placeOrder() {

    if (!this.deliveryAddress.trim()) {

      Swal.fire(
        'Required',
        'Please enter a delivery address.',
        'warning'
      );

      return;

    }

    this.orderService.create({

      deliveryAddress: this.deliveryAddress

    }).subscribe({

      next: () => {

        Swal.fire({

          icon: 'success',

          title: 'Order Placed',

          text: 'Your order has been placed successfully.',

          timer: 1800,

          showConfirmButton: false

        });

        this.router.navigate(['/orders']);

      },

      error: () => {

        Swal.fire(

          'Error',

          'Unable to place order.',

          'error'

        );

      }

    });

  }

}