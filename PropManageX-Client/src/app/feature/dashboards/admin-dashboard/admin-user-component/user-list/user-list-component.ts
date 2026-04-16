import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { AdminUserService, UserDto } from '../admin-user-service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './user-list-component.html',
  styleUrl: './user-list-component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    class: 'page-wrapper'
  }
})
export class UserListComponent implements OnInit {
  private adminUserService = inject(AdminUserService);

  users = signal<UserDto[]>([]);
  isLoading = signal<boolean>(true);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.fetchUsers();
  }

  fetchUsers(): void {
    this.isLoading.set(true);
    this.error.set(null);

    this.adminUserService.getAllUsers().subscribe({
      next: (data) => {
        this.users.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        this.error.set('Failed to load user data from the server.');
        this.isLoading.set(false);
      }
    });
  }

  deleteUser(id: number, name: string): void {
    const confirmDelete = window.confirm(`Are you sure you want to permanently delete ${name}? This action cannot be undone.`);

    if (confirmDelete) {
      this.adminUserService.deleteUser(id).subscribe({
        next: (res) => {
          console.log('Delete Success:', res);
          this.fetchUsers();
        },
        error: (err) => {
          console.error('Delete Error:', err);
          alert('Failed to delete user. They might have active dependencies in the system.');
        }
      });
    }
  }
}