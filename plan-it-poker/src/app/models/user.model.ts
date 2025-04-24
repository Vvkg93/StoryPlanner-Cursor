export interface User {
    id: string;
    email: string;
    displayName: string;
    avatar: string;
    role: 'Admin' | 'User' | 'Viewer';
    createdAt: Date;
    lastLoginAt?: Date;
} 