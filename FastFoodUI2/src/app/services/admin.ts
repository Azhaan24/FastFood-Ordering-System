import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private http = inject(HttpClient);

  private api = environment.api + '/admin';

  getDashboard() {
    return this.http.get(`${this.api}/dashboard`);
  }

  getUsers() {
    return this.http.get(`${this.api}/users`);
  }

  updateRole(id: string, role: string) {
    return this.http.put(
      `${this.api}/users/${id}/role`,
      { role }
    );
  }

  getOrders() {
    return this.http.get(`${this.api}/orders`);
  }

  updateOrder(id: number, status: string) {
    return this.http.put(
      `${this.api}/orders/${id}`,
      { status }
    );
  }

}