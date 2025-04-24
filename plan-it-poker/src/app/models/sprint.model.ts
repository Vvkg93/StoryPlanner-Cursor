export interface Sprint {
    id: string;
    name: string;
    startDate: Date;
    endDate: Date;
    userStories: UserStory[];
}

export interface UserStory {
    id: string;
    name: string;
    description: string;
    averageEstimation: number;
    status: 'pending' | 'completed';
    joinLink: string;
    sprintId: string;
    votes: Vote[];
    createdBy: string;
}

export interface Vote {
    userId: string;
    userName: string;
    userAvatar: string;
    estimation: number;
} 