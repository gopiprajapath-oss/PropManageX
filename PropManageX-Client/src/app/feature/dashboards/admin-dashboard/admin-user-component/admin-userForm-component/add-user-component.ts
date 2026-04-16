import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AdminUserService, CreateUserDto } from '../admin-user-service';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './add-user-component.html',
  styleUrl: './add-user-component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: { class: 'page-wrapper' }
})
export class AddUserComponent {
  private fb = inject(FormBuilder);
  private adminUserService = inject(AdminUserService);
  private router = inject(Router);

  isSubmitting = signal(false);
  error = signal<string | null>(null);

  availableRoles = ['Admin', 'Manager', 'Agent', 'Tenant', 'Finance Analyst'];

  form = this.fb.nonNullable.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', [Validators.required, Validators.pattern('^[0-9\\-\\+]{9,15}$')]],
    role: ['', [Validators.required]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    this.error.set(null);

    const newUser: CreateUserDto = this.form.getRawValue();

    this.adminUserService.addUser(newUser).subscribe({
      next: (response) => {
        console.log('Success:', response);
        this.isSubmitting.set(false);
        this.router.navigate(['/admin-dashboard/users']); 
      },
      error: (err) => {
        console.error('Error adding user:', err);
        this.isSubmitting.set(false);
        this.error.set(err.error || 'Failed to create user. Email may already exist.');
      }
    });
  }
}