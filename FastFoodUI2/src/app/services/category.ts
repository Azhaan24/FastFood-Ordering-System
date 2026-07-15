import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Category } from '../models/category';

@Injectable({
providedIn:'root'
})

export class CategoryService{
    private http=inject(HttpClient);
    private api=environment.apiV1+"/category";
    
    getAll():Observable<Category[]>{
        return this.http.get<Category[]>(this.api);
    }
    
    get(id:number){
        return this.http.get<Category>(`${this.api}/${id}`);
    }
    
    create(data:any){
        return this.http.post(this.api,data);
    }
    
    update(id:number,data:any){
        return this.http.put(`${this.api}/${id}`,data);
    }
    
    delete(id:number){
        return this.http.delete(`${this.api}/${id}`);
    }
}