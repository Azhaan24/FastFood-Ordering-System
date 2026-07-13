import { Injectable, inject } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

import { Order, CreateOrder } from '../models/order';

@Injectable({

  providedIn: 'root'

})

export class OrderService {

  private http = inject(HttpClient);

  private api = environment.apiV1 + '/Orders';

  getOrders(): Observable<Order[]> {

    return this.http.get<Order[]>(this.api);

  }

  getOrder(id: number): Observable<Order> {

    return this.http.get<Order>(`${this.api}/${id}`);

  }

  create(data: CreateOrder) {

    return this.http.post(this.api, data);

  }

  cancel(id: number) {

    return this.http.put(`${this.api}/${id}/cancel`, {});

  }

}