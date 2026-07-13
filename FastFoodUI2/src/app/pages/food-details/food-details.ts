import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import Swal from 'sweetalert2';

import { FoodService } from '../../services/food';
import { CartService } from '../../services/cart';

import { Food } from '../../models/food';

@Component({
  selector: 'app-food-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './food-details.html',
  styleUrl: './food-details.css'
})
export class FoodDetailsComponent implements OnInit {

  private route = inject(ActivatedRoute);

  private foodService = inject(FoodService);

  private cartService = inject(CartService);

  food?: Food;

  loading = true;

  ngOnInit(): void {

    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.foodService.getById(id).subscribe({

      next: (food) => {

        this.food = food;

        this.loading = false;

      },

      error: () => {

        this.loading = false;

      }

    });

  }

  addToCart() {

    if (!this.food) return;

    this.cartService.add(this.food.id).subscribe({

      next: () => {

        Swal.fire({

          icon: 'success',

          title: 'Added',

          text: 'Food added to cart',

          timer: 1500,

          showConfirmButton: false

        });

      }

    });

  }

}