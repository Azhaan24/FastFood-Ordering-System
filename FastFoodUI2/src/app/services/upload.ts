import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UploadService {

  private http = inject(HttpClient);

  private api = environment.apiV1 + '/Upload';

  upload(file: FormData) {
    return this.http.post<any>(this.api, file);
  }

}