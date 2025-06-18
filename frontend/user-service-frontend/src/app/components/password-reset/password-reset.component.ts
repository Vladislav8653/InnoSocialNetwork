import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ResetPasswordDto } from '../../models/auth.model';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent {
  resetForm: FormGroup;
  requestForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';
  isLoading: boolean = false;
  isRequestMode: boolean = true;
  token: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.requestForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });

    this.resetForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    }, {
      validators: this.passwordMatchValidator
    });

    this.token = this.route.snapshot.queryParamMap.get('token');
    if (this.token) {
      this.isRequestMode = false;
    }
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('newPassword');
    const confirmPassword = form.get('confirmPassword');
    
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
    }
  }

  onRequestReset(): void {
    if (this.requestForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';
      this.successMessage = '';

      this.authService.requestPasswordReset(this.requestForm.value.email).subscribe({
        next: () => {
          this.successMessage = 'Password reset instructions have been sent to your email';
          this.requestForm.reset();
        },
        error: (error) => {
          this.errorMessage = error.error.message || 'An error occurred while requesting password reset';
        },
        complete: () => {
          this.isLoading = false;
        }
      });
    }
  }

  onResetPassword(): void {
    if (this.resetForm.valid && this.token) {
      this.isLoading = true;
      this.errorMessage = '';
      this.successMessage = '';

      const resetData: ResetPasswordDto = {
        email: this.requestForm.value.email,
        newPassword: this.resetForm.value.newPassword,
        resetToken: this.token
      };

      this.authService.resetPassword(resetData).subscribe({
        next: () => {
          this.successMessage = 'Password has been reset successfully';
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        error: (error) => {
          this.errorMessage = error.error.message || 'An error occurred while resetting password';
        },
        complete: () => {
          this.isLoading = false;
        }
      });
    }
  }
} 