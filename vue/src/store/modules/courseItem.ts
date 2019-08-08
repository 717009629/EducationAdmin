import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import CourseItem from '../entities/courseItem'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface CourseItemState extends ListState<CourseItem>{
    editCourseItem:CourseItem
}
class CourseItemMutations extends ListMutations<CourseItem>{

}
class CourseItemModule extends ListModule<CourseItemState,any,CourseItem>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<CourseItem>(),
        loading:false,
        editCourseItem:new CourseItem()
    }
    actions={
        async getAll(context:ActionContext<CourseItemState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/CourseItem/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<CourseItem>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<CourseItemState,any>,payload:any){
            await Ajax.post('/api/services/app/CourseItem/Create',payload.data);
        },
        async update(context:ActionContext<CourseItemState,any>,payload:any){
            await Ajax.put('/api/services/app/CourseItem/Update',payload.data);
        },
        async delete(context:ActionContext<CourseItemState,any>,payload:any){
            await Ajax.delete('/api/services/app/CourseItem/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<CourseItemState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/CourseItem/Get?Id='+payload.id);
            return reponse.data.result as CourseItem;
        }
    };
    mutations={
        setCurrentPage(state:CourseItemState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:CourseItemState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:CourseItemState,courseItem:CourseItem){
            state.editCourseItem=courseItem;
        }
    }
}
const  courseItemModule=new CourseItemModule();
export default courseItemModule;