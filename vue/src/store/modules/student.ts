import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface StudentState extends ListState<Student>{
    editStudent:Student
}
class StudentMutations extends ListMutations<Student>{

}
class StudentModule extends ListModule<StudentState,any,Student>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Student>(),
        loading:false,
        editStudent:new Student()
    }
    actions={
        async getAll(context:ActionContext<StudentState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Student/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Student>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<StudentState,any>,payload:any){
            await Ajax.post('/api/services/app/Student/Create',payload.data);
        },
        async update(context:ActionContext<StudentState,any>,payload:any){
            await Ajax.put('/api/services/app/Student/Update',payload.data);
        },
        async delete(context:ActionContext<StudentState,any>,payload:any){
            await Ajax.delete('/api/services/app/Student/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<StudentState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Student/Get?Id='+payload.id);
            return reponse.data.result as Student;
        }
    };
    mutations={
        setCurrentPage(state:StudentState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:StudentState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:StudentState,student:Student){
            state.editStudent=student;
        }
    }
}
const studentModule=new StudentModule();
export default studentModule;