import Entity from './entity'
import User from './user';
import Student from './student';
import Order from './order';
import Class from './class';

export default class Lesson extends Entity<number>{
    lessonDate: Date
    classId: number
    clas?: Class
    orderId: number
    order?: Order
    lessonIndex:number
    isFinish: boolean
    teacherId: number
    teacher?: User
    course?: string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}