import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryService } from '../../../services/category';
import { Category } from '../../../models/category';

@Component({
  selector:'app-categories',
  standalone:true,
  imports:[CommonModule],
  templateUrl:'./categories.html'
})
export class CategoriesComponent implements OnInit{

    categories:Category[]=[];

    constructor(private service:CategoryService){}

    ngOnInit(){

        this.load();

    }

    load(){

        this.service.getAll().subscribe({

            next:data=>{

                this.categories=data;

            }

        });

    }

}