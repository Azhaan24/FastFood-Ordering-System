import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminService } from '../../../services/admin';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class DashboardComponent implements OnInit {

  private admin = inject(AdminService);

  data: any;

  ngOnInit() {
    this.admin.getDashboard().subscribe({
      next: res => {
        this.data = res;
      }
    });
  }

}