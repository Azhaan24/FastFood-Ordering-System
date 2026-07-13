import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminService } from '../../../services/admin';

@Component({
  selector: 'app-admin-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders.html'
})
export class OrdersComponent implements OnInit {

  private admin = inject(AdminService);

  orders: any[] = [];

  ngOnInit() {

    this.load();

  }

  load() {

    this.admin.getOrders()

      .subscribe({

        next: (orders: any) => {

          this.orders = orders;

        }

      });

  }

  changeStatus(order: any, status: string) {

    this.admin.updateOrder(order.id, status)

      .subscribe(() => {

        this.load();

      });

  }

}