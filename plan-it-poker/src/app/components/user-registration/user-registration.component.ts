import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class UserRegistrationComponent implements OnInit {
  registrationForm: FormGroup;
  loading = false;
  error: string | null = null;
  sprintId: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.registrationForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      displayName: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  ngOnInit(): void {
    this.sprintId = this.route.snapshot.paramMap.get('sprintId');
  }

  onSubmit(): void {
    if (this.registrationForm.valid) {
      this.loading = true;
      this.error = null;

      this.authService.register(this.registrationForm.value).subscribe({
        next: () => {
          if (this.sprintId) {
            this.router.navigate(['/sprints', this.sprintId]);
          } else {
            this.router.navigate(['/sprints']);
          }
        },
        error: (error) => {
          this.error = 'Registration failed';
          this.loading = false;
          console.error('Registration error:', error);
        }
      });
    }
  }
} 