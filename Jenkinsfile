pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                echo 'Checking out the repository...'
            }
        }

        stage('Build') {
            steps {
                echo 'Simulating build process...'
                sh 'echo "Build completed successfully." > build.log'
            }
        }

        stage('Test') {
            steps {
                echo 'Simulating test process...'
                sh 'echo "<testsuite tests=\\"1\\" failures=\\"0\\"><testcase classname=\\"DummyTest\\" name=\\"Test1\\"/></testsuite>" > test-results.xml'
            }
            post {
                always {
                    junit 'test-results.xml'
                }
            }
        }

        stage('Archive Artifacts') {
            steps {
                echo 'Archiving dummy build artifacts...'
                archiveArtifacts artifacts: 'build.log', allowEmptyArchive: true
            }
        }
    }

    post {
        success {
            echo 'Pipeline completed successfully.'
        }
        failure {
            echo 'Pipeline failed.'
        }
    }
}
