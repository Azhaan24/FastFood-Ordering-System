import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private http = inject(HttpClient);

  private api = environment.apiV1 + '/Reviews';

  getReviews(foodItemId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.api}/${foodItemId}`);
  }

  addReview(review: any): Observable<any> {
    return this.http.post<any>(this.api, review);
  }

  deleteReview(id: number): Observable<any> {
    return this.http.delete<any>(`${this.api}/${id}`);
  }

}