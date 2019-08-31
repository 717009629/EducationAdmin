import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Teacher from '../entities/teacher'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface TeacherState extends ListState<Teacher>{
    editTeacher:Teacher
    listAvailable:  Array<Teacher>
}
class TeacherMutations extends ListMutations<Teacher>{

}
class TeacherModule extends ListModule<TeacherState,any,Teacher>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Teacher>(),
        listAvailable: new Array<Teacher>(),
        loading:false,
        editTeacher:new Teacher()

    }
    actions={
        async getAll(context:ActionContext<TeacherState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Teacher/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Teacher>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async getAllAvailable(context:ActionContext<TeacherState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Teacher/GetAllAvailable',{params:payload.data});
            context.state.listAvailable=reponse.data.result;
        },
        async get(context:ActionContext<TeacherState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Teacher/Get?Id='+payload.id);
            return reponse.data.result as Teacher;
        }
    };
    mutations={
        setCurrentPage(state:TeacherState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:TeacherState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:TeacherState,teacher:Teacher){
            state.editTeacher=teacher;
        }
    }
}
const  teacherModule=new TeacherModule();
export default teacherModule;