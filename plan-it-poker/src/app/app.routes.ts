import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { SprintDashboardComponent } from './components/sprint-dashboard/sprint-dashboard.component';
import { UserStoryVotingComponent } from './components/user-story-voting/user-story-voting.component';
import { CreateSprintComponent } from './components/admin/create-sprint/create-sprint.component';
import { CreateUserStoryComponent } from './components/admin/create-user-story/create-user-story.component';
import { UserRegistrationComponent } from './components/user-registration/user-registration.component';
import { AdminGuard } from './guards/admin.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: UserRegistrationComponent },
  { path: 'register/:sprintId', component: UserRegistrationComponent },
  { path: 'sprints', component: SprintDashboardComponent },
  { path: 'sprints/:id', component: UserStoryVotingComponent },
  { 
    path: 'admin/sprints/create', 
    component: CreateSprintComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'admin/sprints/:sprintId/stories/create',
    component: CreateUserStoryComponent,
    canActivate: [AdminGuard]
  },
  { path: 'admin/create-user-story/:sprintId', component: UserStoryVotingComponent, canActivate: [AdminGuard] },
  { path: '**', redirectTo: '/login' }
];
