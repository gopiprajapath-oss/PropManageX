import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { TokenService } from '../../../core/services/token.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    'class': 'auth-page-container'
  }
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private tokenService = inject(TokenService);
  private router = inject(Router);

  loading = signal(false);
  error = signal<string | null>(null);

  form = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    this.authService.login(this.form.getRawValue()).subscribe({
      next: (respons: any) => {
  this.loading.set(false);
  localStorage.setItem('token', respons.token);

  const user = this.tokenService.getUser();

  switch (user?.role) {
    case 'Manager':
      this.router.navigate(['/manager-dashboard']);
      break;

    case 'Agent':
      this.router.navigate(['/agent-dashboard']);
      break;

    case 'Admin':
      this.router.navigate(['/admin-dashboard']);
      break;

    case 'Finance Analyst':
      this.router.navigate(['/finance-dashboard']);
      break;

    case 'User':
    case 'Tenant':
    case 'Buyer':  
      this.router.navigate(['/user-dashboard/properties']);
      break;


    default:
      this.router.navigate(['/']);
      break;
  }
},

      error: (err) => {
        this.loading.set(false);
        this.error.set(err.error?.message || 'Authentication failed. Please check your credentials.');
      }
    });
  }
}