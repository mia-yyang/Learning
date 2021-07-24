using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    // 建造者模式  构建者模式  生成器模式
    // 如果不使用建造者模式，采用先创建后 set 的方式，
    // 那就会导致在第一个 set 之后，对象处于无效状态。

    //Rectangle r = new Rectange(); // r is invalid
    //r.setWidth(2); // r is invalid
    //r.setHeight(3); // r is valid


    // 建造者模式是让建造者类来负责对象的创建工作
    // 工厂模式是由工厂类来负责对象创建的工作

    // 两者区别
    // 工厂模式是用来创建不同但是相关类型的对象（继承同一父类或者接口的一组子类），
    // 由给定的参数来决定创建哪种类型的对象。

    // 建造者模式是用来创建一种类型的复杂对象，通过设置不同的可选参数，“定制化”地创建不同的对象。



}
