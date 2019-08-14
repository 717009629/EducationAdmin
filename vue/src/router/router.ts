declare global {
    interface RouterMeta {
        title: string;
    }
    interface Router {
        path: string;
        name: string;
        icon?: string;
        permission?: string;
        meta?: RouterMeta;
        component: any;
        children?: Array<Router>;
    }
    interface System {
        import(request: string): Promise<any>
    }
    var System: System
}
import login from '../views/login.vue'
import home from '../views/home/home.vue'
import main from '../views/main.vue'

export const locking = {
    path: '/locking',
    name: 'locking',
    component: () => import('../components/lockscreen/components/locking-page.vue')
};
export const loginRouter: Router = {
    path: '/',
    name: 'login',
    meta: {
        title: 'LogIn'
    },
    component: () => import('../views/login.vue')
};
export const otherRouters: Router = {
    path: '/main',
    name: 'main',
    permission: '',
    meta: { title: 'ManageMenu' },
    component: main,
    children: [
        { path: 'home', meta: { title: 'HomePage' }, name: 'home', component: () => import('../views/home/home.vue') }
    ]
}
export const appRouters: Array<Router> = [{
    path: '/setting',
    name: 'setting',
    permission: '',
    meta: { title: 'ManageMenu' },
    icon: '&#xe68a;',
    component: main,
    children: [
        { path: 'user', permission: 'Pages.Users', meta: { title: 'Users' }, name: 'user', component: () => import('../views/setting/user/user.vue') },
        { path: 'role', permission: 'Pages.Roles', meta: { title: 'Roles' }, name: 'role', component: () => import('../views/setting/role/role.vue') },
        { path: 'tenant', permission: 'Pages.Tenants', meta: { title: 'Tenants' }, name: 'tenant', component: () => import('../views/setting/tenant/tenant.vue') }
    ]
},
{
    path: '/students',
    name: 'stundents',
    permission: '',
    meta: { title: 'StudentsManage' },
    icon: '&#xe753;',
    component: main,
    children: [
        { path: 'student', permission: 'Pages.Students', meta: { title: 'Students' }, name: 'student', component: () => import('../views/students/student/student.vue') },
       
    ]
},
{
    path: '/sales',
    name: 'sales',
    permission: '',
    meta: { title: 'SalesManage' },
    icon: '&#xe6ed;',
    component: main,
    children: [
        { path: 'record', permission: 'Pages.Records', meta: { title: 'Records' }, name: 'record', component: () => import('../views/sales/record/record.vue') },
        { path: 'order', permission: 'Pages.Orders', meta: { title: 'Orders' }, name: 'order', component: () => import('../views/sales/order/order.vue') },
        { path: 'contract', permission: 'Pages.Contracts', meta: { title: 'Contracts' }, name: 'contract', component: () => import('../views/sales/contract/contract.vue') }
    ]
},
{
    path: '/education',
    name: 'education',
    permission: '',
    meta: { title: 'EducationManage' },
    icon: '&#xe67c;',
    component: main,
    children: [
        { path: 'course', permission: 'Pages.Courses', meta: { title: 'Courses' }, name: 'course', component: () => import('../views/education/course/course.vue') }
    ]
}]
export const routers = [
    loginRouter,
    locking,
    ...appRouters,
    otherRouters
];
