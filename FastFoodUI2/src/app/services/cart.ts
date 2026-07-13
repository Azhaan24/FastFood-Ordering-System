import { Injectable, inject } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn:'root'
})
export class CartService{

    private http=inject(HttpClient);

    private api=environment.api+"/Cart";

    getCart(){

        return this.http.get(this.api);

    }

    add(foodItemId:number){

        return this.http.post(

            this.api,

            {

                foodItemId,

                quantity:1

            }

        );

    }

    update(id:number,quantity:number){

        return this.http.put(

            `${this.api}/${id}`,

            {

                quantity

            }

        );

    }

    remove(id:number){

        return this.http.delete(

            `${this.api}/${id}`

        );

    }

}