import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { FoodService } from '../../services/food';
import { CartService } from '../../services/cart';
import { ReviewService } from '../../services/review';
import { Food } from '../../models/food';

@Component({
  selector: 'app-food-details',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './food-details.html',
  styleUrl: './food-details.css'
})

export class FoodDetailsComponent implements OnInit {
  
  private route = inject(ActivatedRoute);
  private foodService = inject(FoodService);
  private cartService = inject(CartService);
  private reviewService = inject(ReviewService);

  food?: Food;
  loading = true;
  rating = 5;
  comment = '';
  reviews: any[] = [];

  ngOnInit(): void {

    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.foodService.getById(id).subscribe({
      next: (food) => {
        this.food = food;
        this.loading = false;
        this.loadReviews(id);
      },
      error: () => {
        this.loading = false;
        Swal.fire(
          'Error',
          'Unable to load food details.',
          'error'
        );
      }
    });
  }

  loadReviews(foodId: number): void {
    this.reviewService.getReviews(foodId).subscribe({
      next: (res: any) => {
        this.reviews = res;
      },
      error: () => {
        this.reviews = [];
      }
    });
  }

  addToCart(): void {
    if (!this.food)
      return;
    this.cartService.add(this.food.id).subscribe({
      next: () => {
        Swal.fire({
          icon: 'success',
          title: 'Added',
          text: 'Food added to cart',
          timer: 1500,
          showConfirmButton: false
        });
      },

      error: () => {
        Swal.fire(
          'Error',
          'Unable to add item to cart.',
          'error'
        );
      }
    });
  }

  submitReview(): void {
    if (!this.food)
      return;
    const review = {
      foodItemId: this.food.id,
      rating: this.rating,
      comment: this.comment
    };

    this.reviewService.addReview(review).subscribe({
      next: () => {
        Swal.fire({
          icon: 'success',
          title: 'Review Submitted',
          text: 'Thank you for your feedback.',
          timer: 1500,
          showConfirmButton: false
        });

        this.rating = 5;
        this.comment = '';
        this.loadReviews(this.food!.id);
      },

      error: () => {
        Swal.fire(
          'Error',
          'Unable to submit review.',
          'error'
        );
      }
    });
  }
}