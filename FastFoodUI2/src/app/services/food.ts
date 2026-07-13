import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { Food } from '../models/food';

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  private http = inject(HttpClient);

  private api = environment.apiV1 + '/FoodItems';

  getAll(
    search?: string,
    categoryId?: number,
    page = 1,
    pageSize = 12
  ): Observable<any> {

    let params = new HttpParams()
      .set('pageNumber', page)
      .set('pageSize', pageSize);

    if (search) {
      params = params.set('search', search);
    }

    if (categoryId) {
      params = params.set('categoryId', categoryId);
    }

    return this.http.get<any>(this.api, {
      params
    });

  }

  getById(id: number): Observable<Food> {

    return this.http.get<Food>(`${this.api}/${id}`);

  }

  create(form: FormData) {

    return this.http.post(this.api, form);

  }

  update(id: number, form: FormData) {

    return this.http.put(`${this.api}/${id}`, form);

  }

  delete(id: number) {

    return this.http.delete(`${this.api}/${id}`);

  }

}