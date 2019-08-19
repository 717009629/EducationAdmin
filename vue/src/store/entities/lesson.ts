import Entity from './entity'
import User from './user';
import Student from './student';
import Order from './order';

export default class Lesson extends Entity<number>{
    lessonDate: Date
    studentId: number
    student?: Student
    orderId: number
    order?: Order
    isFinish: boolean
    teacherId: number
    teacher?: User
    course?: string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}