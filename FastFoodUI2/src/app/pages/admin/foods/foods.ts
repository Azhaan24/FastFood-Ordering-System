import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import Swal from 'sweetalert2';

import { FoodService } from '../../../services/food';
import { CategoryService } from '../../../services/category';

@Component({
  selector: 'app-foods',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './foods.html',
  styleUrl: './foods.css'
})
export class FoodsComponent implements OnInit {

  private foodService = inject(FoodService);
  private categoryService = inject(CategoryService);

  foods: any[] = [];
  categories: any[] = [];

  form: any = {};

  selectedFile?: File;

  editing = false;

  editId = 0;

  ngOnInit() {

    this.loadFoods();

    this.loadCategories();

  }

  loadFoods() {

    this.foodService.getAll().subscribe({

      next: (res: any) => {

        this.foods = res.items ?? res;

      }

    });

  }

  loadCategories() {

    this.categoryService.getAll().subscribe({

      next: (res: any) => {

        this.categories = res;

      }

    });

  }

  choose(event: any) {

    this.selectedFile = event.target.files[0];

  }

  save() {

    const formData = new FormData();

    formData.append('name', this.form.name);
    formData.append('description', this.form.description);
    formData.append('price', this.form.price);
    formData.append('categoryId', this.form.categoryId);

    if (this.selectedFile) {

      formData.append('image', this.selectedFile);

    }

    if (this.editing) {

      this.foodService.update(this.editId, formData)

        .subscribe(() => {

          Swal.fire('Success', 'Food Updated', 'success');

          this.reset();

        });

    } else {

      this.foodService.create(formData)

        .subscribe(() => {

          Swal.fire('Success', 'Food Added', 'success');

          this.reset();

        });

    }

  }

  edit(food: any) {

    this.form = { ...food };

    this.editId = food.id;

    this.editing = true;

  }

  delete(food: any) {

    Swal.fire({

      title: 'Delete Food?',

      icon: 'warning',

      showCancelButton: true

    }).then(result => {

      if (!result.isConfirmed) return;

      this.foodService.delete(food.id)

        .subscribe(() => {

          Swal.fire('Deleted', '', 'success');

          this.loadFoods();

        });

    });

  }

  reset() {

    this.form = {};

    this.editId = 0;

    this.editing = false;

    this.selectedFile = undefined;

    this.loadFoods();

  }

}