import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SprintService } from '../../services/sprint.service';
import { Sprint } from '../../models/sprint.model';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-sprint-dashboard',
  templateUrl: './sprint-dashboard.component.html',
  standalone: true,
  imports: [
    CommonModule, 
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDividerModule,
    MatChipsModule,
    MatTooltipModule
  ],
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(20px)' }),
        animate('300ms ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ]),
    trigger('slideIn', [
      transition(':enter', [
        style({ transform: 'translateX(-100%)' }),
        animate('300ms ease-out', style({ transform: 'translateX(0)' }))
      ])
    ])
  ]
})
export class SprintDashboardComponent implements OnInit {
  sprints: Sprint[] = [];
  loading = true;
  error: string | null = null;
  isAdmin = false;

  constructor(
    private sprintService: SprintService,
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.loadSprints();
    this.isAdmin = this.authService.isAdmin();
  }

  loadSprints(): void {
    this.loading = true;
    this.sprintService.getAllSprints().subscribe({
      next: (sprints) => {
        this.sprints = sprints;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load sprints';
        this.loading = false;
        console.error('Error loading sprints:', error);
      }
    });
  }

  viewSprintDetails(sprintId: string): void {
    this.router.navigate(['/sprints', sprintId]);
  }

  createNewSprint(): void {
    this.router.navigate(['/admin/sprints/create']);
  }

  isSprintActive(sprint: Sprint): boolean {
    const now = new Date();
    return new Date(sprint.startDate) <= now && new Date(sprint.endDate) >= now;
  }
} 