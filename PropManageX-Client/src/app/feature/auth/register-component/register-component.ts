import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { RegisterDto } from '../models/register.dto';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule],
  templateUrl: './register-component.html',
  styleUrl: './register-component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    'class': 'auth-page-wrapper'
  }
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);

  loading = signal(false);
  error = signal<string | null>(null);

  form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    role: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    const dto: RegisterDto = this.form.getRawValue();

    this.authService.register(dto).subscribe({
      next: (res) => {
        this.loading.set(false);
        alert(res.message || 'Registeration Sucessfull')
        // Implement navigation or success toast here
      },
      error: (err) => {
        this.loading.set(false);
        this.error.set(err?.message || 'Registration failed. Please try again.');
      }
    });
  }
}