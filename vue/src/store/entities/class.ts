import Entity from "./entity";
import Order from "./order";
import Lesson from "./lesson";
import Course from "./course";
import Teacher from "./teacher";

export default class Class extends Entity<number>{
    name?: string   
    orders?: Array<Order>
    lessons?: Array<Lesson>
    courseId?: number
    course?: Course
    teacher?: Teacher
    teacherId?: number
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}