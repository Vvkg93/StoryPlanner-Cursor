import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SprintService } from '../../../services/sprint.service';
import { Sprint } from '../../../models/sprint.model';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-create-user-story',
  templateUrl: './create-user-story.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class CreateUserStoryComponent implements OnInit {
  userStoryForm: FormGroup;
  sprint: Sprint | null = null;
  loading = true;
  submitting = false;
  error: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private sprintService: SprintService,
    public router: Router,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {
    this.userStoryForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    const sprintId = this.route.snapshot.paramMap.get('sprintId');
    if (sprintId) {
      this.loadSprint(sprintId);
    }
  }

  loadSprint(id: string): void {
    this.loading = true;
    this.sprintService.getSprint(id).subscribe({
      next: (sprint: Sprint) => {
        this.sprint = sprint;
        this.loading = false;
      },
      error: (error: any) => {
        this.error = 'Failed to load sprint';
        this.loading = false;
        console.error('Error loading sprint:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.userStoryForm.valid && this.sprint) {
      this.submitting = true;
      this.error = null;

      const userStory = {
        ...this.userStoryForm.value,
        sprintId: this.sprint.id,
        createdBy: this.authService.getCurrentUser()?.email || ''
      };

      this.sprintService.createUserStory(userStory).subscribe({
        next: () => {
          this.router.navigate(['/sprints', this.sprint!.id]);
        },
        error: (error: any) => {
          this.error = error.message || 'Failed to create user story';
          this.submitting = false;
          console.error('Error creating user story:', error);
        }
      });
    }
  }
} 