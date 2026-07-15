import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  private http = inject(HttpClient);

  private api = environment.apiV1 + '/Payments';

  create(data: any) {
    return this.http.post(this.api, data);
  }

  verify(data: any) {
    return this.http.post(`${this.api}/verify`, data);
  }

}