import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { Sprint, UserStory, Vote } from '../models/sprint.model';
import { environment } from '../../environments/environment';
import { catchError, map } from 'rxjs/operators';

interface SprintDto {
  id: string;
  name: string;
  startDate: string;
  endDate: string;
  userStories: UserStoryDto[];
}

interface UserStoryDto {
  id: string;
  name: string;
  description: string;
  averageEstimation: number;
  status: 'pending' | 'completed';
  joinLink: string;
  sprintId: string;
  votes: VoteDto[];
  createdBy: string;
}

interface VoteDto {
  userId: string;
  userName: string;
  userAvatar: string;
  estimation: number;
}

@Injectable({
  providedIn: 'root'
})
export class SprintService {
  private apiUrl = `${environment.apiUrl}/api`;

  constructor(private http: HttpClient) { }

  getAllSprints(): Observable<Sprint[]> {
    return this.http.get<SprintDto[]>(`${this.apiUrl}/Sprint`).pipe(
      map(sprints => sprints.map(this.mapSprintDtoToSprint)),
      catchError(error => {
        console.error('Error fetching sprints:', error);
        return of(this.getMockSprints());
      })
    );
  }

  getSprint(id: string): Observable<Sprint> {
    return this.http.get<SprintDto>(`${this.apiUrl}/Sprint/${id}`).pipe(
      map(this.mapSprintDtoToSprint),
      catchError(error => {
        console.error('Error fetching sprint:', error);
        return of(this.getMockSprints().find(s => s.id === id) || this.getMockSprints()[0]);
      })
    );
  }

  createSprint(sprint: Partial<Sprint>): Observable<Sprint> {
    const command = {
      name: sprint.name,
      startDate: sprint.startDate?.toISOString(),
      endDate: sprint.endDate?.toISOString()
    };

    return this.http.post<string>(`${this.apiUrl}/Sprint`, command).pipe(
      map(id => ({
        id,
        name: sprint.name || 'New Sprint',
        startDate: sprint.startDate || new Date(),
        endDate: sprint.endDate || new Date(Date.now() + 14 * 24 * 60 * 60 * 1000),
        userStories: []
      })),
      catchError(error => {
        console.error('Error creating sprint:', error);
        const newSprint: Sprint = {
          id: Math.random().toString(36).substr(2, 9),
          name: sprint.name || 'New Sprint',
          startDate: new Date(sprint.startDate || new Date()),
          endDate: new Date(sprint.endDate || new Date(Date.now() + 14 * 24 * 60 * 60 * 1000)),
          userStories: []
        };
        return of(newSprint);
      })
    );
  }

  updateSprint(id: string, sprint: Partial<Sprint>): Observable<Sprint> {
    const command = {
      id,
      name: sprint.name,
      startDate: sprint.startDate?.toISOString(),
      endDate: sprint.endDate?.toISOString()
    };

    return this.http.put<void>(`${this.apiUrl}/Sprint/${id}`, command).pipe(
      map(() => ({
        id,
        name: sprint.name || 'Updated Sprint',
        startDate: new Date(sprint.startDate || new Date()),
        endDate: new Date(sprint.endDate || new Date(Date.now() + 14 * 24 * 60 * 60 * 1000)),
        userStories: []
      })),
      catchError(error => {
        console.error('Error updating sprint:', error);
        return of({
          id,
          name: sprint.name || 'Updated Sprint',
          startDate: new Date(sprint.startDate || new Date()),
          endDate: new Date(sprint.endDate || new Date(Date.now() + 14 * 24 * 60 * 60 * 1000)),
          userStories: []
        });
      })
    );
  }

  deleteSprint(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Sprint/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting sprint:', error);
        return of(void 0);
      })
    );
  }

  createUserStory(userStory: Partial<UserStory>): Observable<UserStory> {
    const command = {
      name: userStory.name,
      description: userStory.description,
      sprintId: userStory.sprintId,
      createdBy: userStory.createdBy || ''
    };

    return this.http.post<UserStory>(`${this.apiUrl}/UserStory`, command).pipe(
      catchError(error => {
        console.error('Error creating user story:', error);
        return throwError(() => new Error('Failed to create user story'));
      })
    );
  }

  updateUserStory(id: string, userStory: Partial<UserStory>): Observable<UserStory> {
    const command = {
      id,
      name: userStory.name,
      description: userStory.description,
      status: userStory.status
    };

    return this.http.put<void>(`${this.apiUrl}/UserStory/${id}`, command).pipe(
      map(() => ({
        id,
        name: userStory.name || 'Updated User Story',
        description: userStory.description || '',
        averageEstimation: 0,
        status: 'pending' as const,
        joinLink: 'mock-join-link',
        sprintId: userStory.sprintId || '',
        createdBy: userStory.createdBy || '',
        votes: []
      })),
      catchError(error => {
        console.error('Error updating user story:', error);
        return throwError(() => new Error('Failed to update user story'));
      })
    );
  }

  deleteUserStory(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/UserStory/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting user story:', error);
        return of(void 0);
      })
    );
  }

  submitVote(userStoryId: string, points: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/UserStory/${userStoryId}/votes`, { points }).pipe(
      catchError(error => {
        console.error('Error submitting vote:', error);
        return of(void 0);
      })
    );
  }

  private mapSprintDtoToSprint(dto: SprintDto): Sprint {
    return {
      id: dto.id,
      name: dto.name,
      startDate: new Date(dto.startDate),
      endDate: new Date(dto.endDate),
      userStories: dto.userStories.map(us => ({
        id: us.id,
        name: us.name,
        description: us.description,
        averageEstimation: us.averageEstimation,
        status: us.status,
        joinLink: us.joinLink,
        sprintId: us.sprintId,
        createdBy: us.createdBy,
        votes: us.votes.map(v => ({
          userId: v.userId,
          userName: v.userName,
          userAvatar: v.userAvatar,
          estimation: v.estimation
        }))
      }))
    };
  }

  private getMockSprints(): Sprint[] {
    return [
      {
        id: '1',
        name: 'Sprint 1',
        startDate: new Date(Date.now() - 7 * 24 * 60 * 60 * 1000),
        endDate: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000),
        userStories: [
          {
            id: '1',
            name: 'User Story 1',
            description: 'Description for User Story 1',
            averageEstimation: 5,
            status: 'pending',
            joinLink: 'mock-join-link-1',
            sprintId: '1',
            createdBy: 'user@example.com',
            votes: []
          }
        ]
      },
      {
        id: '2',
        name: 'Sprint 2',
        startDate: new Date(Date.now() + 14 * 24 * 60 * 60 * 1000),
        endDate: new Date(Date.now() + 28 * 24 * 60 * 60 * 1000),
        userStories: []
      }
    ];
  }
} 