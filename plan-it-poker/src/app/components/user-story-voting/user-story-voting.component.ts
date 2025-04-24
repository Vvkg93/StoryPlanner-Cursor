import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SprintService } from '../../services/sprint.service';
import { Sprint, UserStory } from '../../models/sprint.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-user-story-voting',
  templateUrl: './user-story-voting.component.html',
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class UserStoryVotingComponent implements OnInit {
  sprint: Sprint | null = null;
  currentUserStory: UserStory | null = null;
  selectedStory: UserStory | null = null;
  storyboardUrl: string = '';
  estimation: number | null = null;
  points: number[] = [1, 2, 3, 5, 8, 13, 21];
  loading = true;
  error: string | null = null;
  isAdmin = false;
  canVote = true;

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private sprintService: SprintService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const sprintId = this.route.snapshot.paramMap.get('id');
    if (sprintId) {
      this.loadSprint(sprintId);
      this.storyboardUrl = window.location.href;
    }
    this.isAdmin = this.authService.isAdmin();
  }

  copyStoryboardUrl(): void {
    navigator.clipboard.writeText(this.storyboardUrl);
  }

  loadSprint(sprintId: string): void {
    this.loading = true;
    this.error = null;
    this.sprintService.getSprint(sprintId).subscribe({
      next: (sprint: Sprint) => {
        this.sprint = sprint;
        this.currentUserStory = sprint.userStories.find(story => story.status === 'pending') || null;
        this.loading = false;
      },
      error: (error: any) => {
        this.error = 'Failed to load sprint';
        this.loading = false;
        console.error('Error loading sprint:', error);
      }
    });
  }

  selectStory(story: UserStory): void {
    if (this.canVote) {
      this.selectedStory = story;
    }
  }

  submitVote(points: number): void {
    if (!this.selectedStory?.id) return;
    
    this.sprintService.submitVote(this.selectedStory.id, points).subscribe({
      next: () => {
        this.estimation = points;
        this.selectedStory = null;
      },
      error: (error: any) => {
        this.error = 'Failed to submit vote';
        console.error('Error submitting vote:', error);
      }
    });
  }

  createUserStory(): void {
    if (!this.sprint?.id) return;
    
    this.router.navigate(['/admin/create-user-story', this.sprint.id]);
  }
} 