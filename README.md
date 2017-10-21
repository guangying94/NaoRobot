# NaoRobot

Nao (pronounced as now) is an autonomous, programmable humanoid robot by SoftBank Group.

More info here: [**Wikipedia on Nao**](https://en.wikipedia.org/wiki/Nao_(robot))

This project is a joint-collaboration with customers to explore the potential use case and how to implement artificial intelligence within Nao.

Here, we will explore the potential of Nao as digital concierge. Breakdown of content as below:

1. Use Case Description
1. Technical Overview of Nao
1. Solution Overview
1. Technology Consideration
1. Next Step

### 1. Use Case Description
Nao looks like a human, or like a little boy. First impression, Nao can be next generation digital concierge, who is able to answer typical concierge questions such as direction, general enquiries etc. Since we can program the motor, it adds more interaction during response. For example, when users are asking for direction, Nao can point to a specific direction on top of the response, i.e. when Nao reply "It's on your left", it will point to left as well.

This use case is straight forward, and a lot of companies are exploring this. One example is collaboration between IBM and Hilton, where IBM leverages Watson to provide AI capabilities to Nao. More info here: [**NAO, IBM create new Hilton concierge**](https://developer.softbankrobotics.com/us-en/showcase/nao-ibm-create-new-hilton-concierge) This project shows the capabilities of Nao with AI, but the main focus here is how users can simplify the process, yet able to leverage on some AI capabilities. IBM Watson may requires subject expertise to train it, and cost of deploying IBM is non-negletable. 

The focus of this project is explore a cost-effective solution to achieve digital concierge.

### 2. Technical Overview of Nao
Nao is running a custom distribution of Linux called **Gentoo**. To develop solutions on Nao, the SDK used is **Choregraphe**. This tool is simple and intuitive, as it is drag-and-drop GUI. Developers just need to connect the modules to create flow. Of course, the build-in tools may not be sufficient, and fortunately, developers have the option to create custom script on **Python**. In short, Nao is a full-fledge system which is capable of running multiple processes. However, the hardware may not be optimized to run complex processes such as articifical intelligence, machine learning and deep learning. 

Apart from processing unit, Nao comes with many sensors that can be configured, for instance LED of his eye, motors around the body, mic, or even camera. With right configuration, all these sensors can provide a human-like experience solution on Nao.

Here's the technical overview of Nao: [**Nao Technical Overview**](http://doc.aldebaran.com/2-1/family/robots/index_robots.html#all-robots)

### 3. Solution Overview


### 4. Technology Consideration


### 5. Next Step
