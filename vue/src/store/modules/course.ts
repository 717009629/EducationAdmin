import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Course from '../entities/course'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface CourseState extends ListState<Course>{
    editCourse:Course
}
class CourseMutations extends ListMutations<Course>{

}
class CourseModule extends ListModule<CourseState,any,Course>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Course>(),
        loading:false,
        editCourse:new Course()
    }
    actions={
        async getAll(context:ActionContext<CourseState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Course/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Course>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<CourseState,any>,payload:any){
            await Ajax.post('/api/services/app/Course/Create',payload.data);
        },
        async update(context:ActionContext<CourseState,any>,payload:any){
            await Ajax.put('/api/services/app/Course/Update',payload.data);
        },
        async delete(context:ActionContext<CourseState,any>,payload:any){
            await Ajax.delete('/api/services/app/Course/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<CourseState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Course/Get?Id='+payload.id);
            return reponse.data.result as Course;
        }
    };
    mutations={
        setCurrentPage(state:CourseState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:CourseState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:CourseState,course:Course){
            state.editCourse=course;
        }
    }
}
const  courseModule=new CourseModule();
export default courseModule;