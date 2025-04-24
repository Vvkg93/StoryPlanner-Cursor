import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { SprintService } from '../../../services/sprint.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-create-sprint',
  templateUrl: './create-sprint.component.html',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ]
})
export class CreateSprintComponent implements OnInit {
  sprintForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private sprintService: SprintService,
    public router: Router
  ) {
    this.sprintForm = this.formBuilder.group({
      name: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required]
    });
  }

  ngOnInit() {
    // Set default dates
    const today = new Date();
    const oneWeekLater = new Date();
    oneWeekLater.setDate(today.getDate() + 7);

    this.sprintForm.patchValue({
      startDate: today,
      endDate: oneWeekLater
    });
  }

  onSubmit(): void {
    if (this.sprintForm.valid) {
      this.loading = true;
      this.error = null;

      const formValue = this.sprintForm.value;
      const sprintData = {
        ...formValue,
        startDate: new Date(formValue.startDate),
        endDate: new Date(formValue.endDate)
      };

      this.sprintService.createSprint(sprintData).subscribe({
        next: () => {
          this.router.navigate(['/sprints']);
        },
        error: (error) => {
          this.error = 'Failed to create sprint';
          this.loading = false;
          console.error('Error creating sprint:', error);
        }
      });
    }
  }
} 