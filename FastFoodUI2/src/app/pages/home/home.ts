import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { FoodService } from '../../services/food';
import { CategoryService } from '../../services/category';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    FormsModule
  ],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class HomeComponent implements OnInit {

  foods: any[] = [];

  categories: any[] = [];

  selectedCategory = 0;

  search = '';

  page = 1;

  totalPages = 1;

  constructor(

    private foodService: FoodService,

    private categoryService: CategoryService

  ) { }

  ngOnInit() {

    this.loadCategories();

    this.loadFoods();

  }

  loadCategories() {

    this.categoryService.getAll().subscribe({

      next: data => {

        this.categories = data;

      }

    });

  }

  loadFoods() {

    this.foodService.getAll(

      this.search,

      this.selectedCategory || undefined,

      this.page

    ).subscribe({

      next: res => {

        this.foods = res.items;

        this.totalPages = res.totalPages;

      }

    });

  }

}