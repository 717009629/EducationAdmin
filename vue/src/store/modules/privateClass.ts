import { Store, Module, ActionContext } from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import PrivateClass from '../entities/privateClass'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface PrivateClassState extends ListState<PrivateClass> {
    editPrivateClass: PrivateClass
    freeList: Array<PrivateClass>
}
class PrivateClassMutations extends ListMutations<PrivateClass>{

}
class PrivateClassModule extends ListModule<PrivateClassState, any, PrivateClass>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<PrivateClass>(),
        freeList: new Array<PrivateClass>(),
        loading: false,
        editPrivateClass: new PrivateClass()
    }
    actions = {
        async getAll(context: ActionContext<PrivateClassState, any>, payload: any) {
            context.state.loading = true;
            let reponse = await Ajax.get('/api/services/app/PrivateClass/GetAll', { params: payload.data });
            context.state.loading = false;
            let page = reponse.data.result as PageResult<PrivateClass>;
            context.state.totalCount = page.totalCount;
            context.state.list = page.items;
        },
        
       
        async finish(context: ActionContext<PrivateClassState, any>, payload: any) {
            await Ajax.post('/api/services/app/PrivateClass/Finish', payload.data);
        },
    };
    mutations = {
        setCurrentPage(state: PrivateClassState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: PrivateClassState, pagesize: number) {
            state.pageSize = pagesize;
        },
        edit(state: PrivateClassState, privateClass: PrivateClass) {
            state.editPrivateClass = privateClass;
        }
    }
}
const privateClassModule = new PrivateClassModule();
export default privateClassModule;