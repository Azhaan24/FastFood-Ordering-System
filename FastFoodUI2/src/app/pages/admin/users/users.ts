import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import Swal from 'sweetalert2';

import { AdminService } from '../../../services/admin';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './users.html',
  styleUrl: './users.css'
})
export class UsersComponent implements OnInit {

  private admin = inject(AdminService);

  users: any[] = [];

  ngOnInit() {
    this.load();
  }

  load() {

    this.admin.getUsers().subscribe({

      next: (users: any) => {

        this.users = users;

      }

    });

  }

  makeAdmin(user: any) {

    this.admin.updateRole(user.id, 'Admin')

      .subscribe(() => {

        Swal.fire(
          'Success',
          'Role Updated',
          'success'
        );

        this.load();

      });

  }

}